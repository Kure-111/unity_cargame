﻿// ----------------------------------------------------------------------------
// <copyright file="ChatConnectionHandler.cs" company="Exit Games GmbH">
//   Chat framework for Photon - Copyright (C) 2018 Exit Games GmbH
// </copyright>
// <summary>
//   If the game logic does not call Service() for whatever reason, this keeps the connection.
// </summary>
// <author>developer@photonengine.com</author>
// ----------------------------------------------------------------------------


#if UNITY_4_7 || UNITY_5 || UNITY_5_3_OR_NEWER
#define SUPPORTED_UNITY
#endif


namespace Photon.Chat
{
    using System;
    using SupportClass = ExitGames.Client.Photon.SupportClass;

    #if SUPPORTED_UNITY
    using UnityEngine;
    #endif


    #if SUPPORTED_UNITY
    public class ChatConnectionHandler : MonoBehaviour
    #else
    public class ChatConnectionHandler
    #endif
    {
        /// <summary>
        /// Photon client to log information and statistics from.
        /// </summary>
        public ChatClient Client { get; set; }

        private byte fallbackThreadId = 255;

        private bool didSendAcks;
        private int startedAckingTimestamp;
        private int deltaSinceStartedToAck;

        /// <summary>Defines for how long the Fallback Thread should keep the connection, before it may time out as usual.</summary>
        /// <remarks>We want to the Client to keep it's connection when an app is in the background (and doesn't call Update / Service Clients should not keep their connection indefinitely in the background, so after some milliseconds, the Fallback Thread should stop keeping it up.</remarks>
        public int KeepAliveInBackground = 30000;

        /// <summary>Counts how often the Fallback Thread called SendAcksOnly, which is purely of interest to monitor if the game logic called SendOutgoingCommands as intended.</summary>
        public int CountSendAcksOnly { get; private set; }

        public bool FallbackThreadRunning
        {
            get { return this.fallbackThreadId < 255; }
        }


        #if SUPPORTED_UNITY
        public bool ApplyDontDestroyOnLoad = true;

        /// <summary></summary>
        protected virtual void Awake()
        {
            if (this.ApplyDontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        /// <summary>Called by Unity when the play mode ends. Used to cleanup.</summary>
        protected virtual void OnDestroy()
        {
            //Debug.Log("OnDestroy on ChatConnectionHandler.");
            this.StopFallbackSendAckThread();
        }

        /// <summary>Called by Unity when the application is closed. Disconnects.</summary>
        protected virtual void OnApplicationQuit()
        {
            //Debug.Log("OnApplicationQuit");
            this.StopFallbackSendAckThread();
            if (this.Client != null)
            {
                this.Client.Disconnect();
                this.Client.chatPeer.StopThread();
            }
            SupportClass.StopAllBackgroundCalls();
        }
        #endif


        public void StartFallbackSendAckThread()
        {
            #if !UNITY_WEBGL
            if (this.FallbackThreadRunning)
            {
                return;
            }

            #if UNITY_SWITCH
            this.fallbackThreadId = SupportClass.StartBackgroundCalls(this.RealtimeFallbackThread, 50);  // as workaround, we don't name the Thread.
            #else
            this.fallbackThreadId = SupportClass.StartBackgroundCalls(this.RealtimeFallbackThread, 50, "RealtimeFallbackThread");
            #endif
            #endif
        }

        public void StopFallbackSendAckThread()
        {
            #if !UNITY_WEBGL
            if (!this.FallbackThreadRunning)
            {
                return;
            }

            SupportClass.StopBackgroundCalls(this.fallbackThreadId);
            this.fallbackThreadId = 255;
            #endif
        }


        /// <summary>A thread which runs independent from the Update() calls. Keeps connections online while loading or in background. See PhotonNetwork.BackgroundTimeout.</summary>
        public bool RealtimeFallbackThread()
        {
            if (this.Client != null)
            {
                if (!this.Client.CanChat)
                {
                    this.didSendAcks = false;
                    return true;
                }

                if (this.Client.chatPeer.ConnectionTime - this.Client.chatPeer.LastSendOutgoingTime > 100)
                {
                    if (this.didSendAcks)
                    {
                        // check if the client should disconnect after some seconds in background
                        this.deltaSinceStartedToAck = Environment.TickCount - this.startedAckingTimestamp;
                        if (this.deltaSinceStartedToAck > this.KeepAliveInBackground)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        this.startedAckingTimestamp = Environment.TickCount;
                    }


                    this.didSendAcks = true;
                    this.CountSendAcksOnly++;
                    this.Client.chatPeer.SendAcksOnly();
                }
                else
                {
                    this.didSendAcks = false;
                }
            }

            return true;
        }
    }
}