//
// Title: IceLink for JavaScript
// Version: 3.9.2.31084
// Copyright Frozen Mountain Software 2011+
//
(function(name, dependencies, definition) {
    if (typeof exports === 'object') {
        for(var i = 0; i < dependencies.length; i++) {
            require('./' + dependencies[i]);
        }
        module.exports = definition();
    } else if (typeof define === 'function' && define.amd) {
        define(name, dependencies, definition);
    } else {
        this[name] = definition();
    }
}('fm.icelink.callstats', ['fm.icelink'], function() {
if (typeof global !== 'undefined' && !global.window) { global.window = global; global.document = { cookie: '' }; }
if (typeof global !== 'undefined' && !global.navigator) { global.navigator = { userAgent: ' ' }; }
this['fm'] = this['fm'] || { };
(function (fm) {
    var icelink;
    (function (icelink) {
        var callstats;
        (function (callstats) {
            /**
            CallStats Client
            */
            var ClientBase = /** @class */ (function () {
                function ClientBase() {
                    var __arguments = new Array(arguments.length);
                    for (var __argumentIndex = 0; __argumentIndex < __arguments.length; ++__argumentIndex) {
                        __arguments[__argumentIndex] = arguments[__argumentIndex];
                    }
                    if (__arguments.length == 0) {
                        //super();
                    }
                    else {
                        throw new fm.icelink.Exception('Constructor overload does not exist with specified parameter count/type combination.');
                    }
                }
                ClientBase.prototype.getTypeString = function () {
                    return '[fm.icelink.callstats.ClientBase]';
                };
                return ClientBase;
            }());
            callstats.ClientBase = ClientBase;
        })(callstats = icelink.callstats || (icelink.callstats = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));
/// <reference path="ClientBase.ts" />
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
function createCallStatsClient() {
    return new callstats();
}

(function (fm) {
    var icelink;
    (function (icelink) {
        var callstats;
        (function (callstats) {
            var Client = /** @class */ (function (_super) {
                __extends(Client, _super);
                function Client(applicationId, applicationSecret) {
                    var _this = _super.call(this) || this;
                    _this._applicationId = applicationId;
                    _this._applicationSecret = applicationSecret;
                    return _this;
                }
                Client.prototype.getTypeString = function () {
                    return '[fm.icelink.callstats.Client]' + ',' + _super.prototype.getTypeString.call(this);
                };
                Client.prototype.addConnection = function (connection, userId, confId, remoteId, deviceId) {
                    if (this._callstats == undefined) {
                        this._callstats = createCallStatsClient();
                        this._callstats.initialize(this._applicationId, this._applicationSecret, userId);
                    }
                    var innerConnection = connection._getInternal();
                    if (fm.icelink.Util.isObjectType(innerConnection, '[fm.icelink.WebRtcConnection]')) {
                        var peerConnection = innerConnection;
                        var nativeConnection = peerConnection.getNativePeerConnection();
                        if (fm.icelink.Util.isNullOrUndefined(nativeConnection)) {
                            fm.icelink.Log.error("Connection needs to be initialized.");
                        }
                        else {
                            this._callstats.addNewFabric(nativeConnection, remoteId, "unbundled", confId);
                        }
                    }
                    else {
                        fm.icelink.Log.error("Connection must use a native RTCPeerConnection.");
                    }
                };
                return Client;
            }(fm.icelink.callstats.ClientBase));
            callstats.Client = Client;
        })(callstats = icelink.callstats || (icelink.callstats = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));
return fm.icelink.callstats;
}));
