//
// Title: IceLink for JavaScript
// Version: 3.9.2.31084
// Copyright Frozen Mountain Software 2011+
//
declare namespace fm.icelink.callstats {
    /**
    CallStats Client
    */
    abstract class ClientBase {
        getTypeString(): string;
        constructor();
        /**
        Start a new callstats session.
        @param connection IceLink connection object.
        @param userId Local peer identifier.
        @param deviceId Local device identifier.
        @param confId Unique conference identifier.
        @param remoteId Remote peer identifier.
        */
        abstract addConnection(connection: fm.icelink.Connection, userId: string, confId: string, remoteId: string, deviceId: string): void;
    }
}
declare class callstats {
    constructor();
    initialize(appId: string, appSecret: string, localUserID: string): void;
    addNewFabric(pcObject: RTCPeerConnection, remoteUserId: string, fabricUsage: string, conferenceID: string): void;
}
declare function createCallStatsClient(): callstats;
declare namespace fm.icelink.callstats {
    class Client extends fm.icelink.callstats.ClientBase {
        private _callstats;
        private _applicationId;
        private _applicationSecret;
        getTypeString(): string;
        constructor(applicationId: string, applicationSecret: string);
        addConnection(connection: fm.icelink.Connection, userId: string, confId: string, remoteId: string, deviceId: string): void;
    }
}
