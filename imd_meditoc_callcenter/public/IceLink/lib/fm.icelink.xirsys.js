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
}('fm.icelink.xirsys', ['fm.icelink'], function() {
if (typeof global !== 'undefined' && !global.window) { global.window = global; global.document = { cookie: '' }; }
if (typeof global !== 'undefined' && !global.navigator) { global.navigator = { userAgent: ' ' }; }
this['fm'] = this['fm'] || { };
(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v2;
            (function (v2) {
                /**
                A XirSys v2 client.
                */
                var Client = /** @class */ (function () {
                    function Client() {
                        fm.icelink.xirsys.v2.Client.fmicelinkxirsysv2ClientInitialize();
                        var __arguments = new Array(arguments.length);
                        for (var __argumentIndex = 0; __argumentIndex < __arguments.length; ++__argumentIndex) {
                            __arguments[__argumentIndex] = arguments[__argumentIndex];
                        }
                        if (__arguments.length == 3) {
                            var ident = __arguments[0];
                            var secret = __arguments[1];
                            var domain = __arguments[2];
                            // chained constructor: Client.call(this, ident, secret, domain, "default");
                            __arguments = new Array(4);
                            __arguments[0] = ident;
                            __arguments[1] = secret;
                            __arguments[2] = domain;
                            __arguments[3] = "default";
                            {
                                var ident_1 = __arguments[0];
                                var secret_1 = __arguments[1];
                                var domain_1 = __arguments[2];
                                var application = __arguments[3];
                                // chained constructor: Client.call(this, ident, secret, domain, application, "default");
                                __arguments = new Array(5);
                                __arguments[0] = ident_1;
                                __arguments[1] = secret_1;
                                __arguments[2] = domain_1;
                                __arguments[3] = application;
                                __arguments[4] = "default";
                                {
                                    var ident_2 = __arguments[0];
                                    var secret_2 = __arguments[1];
                                    var domain_2 = __arguments[2];
                                    var application_1 = __arguments[3];
                                    var room = __arguments[4];
                                    // chained constructor: Client.call(this, ident, secret, domain, application, room, true);
                                    __arguments = new Array(6);
                                    __arguments[0] = ident_2;
                                    __arguments[1] = secret_2;
                                    __arguments[2] = domain_2;
                                    __arguments[3] = application_1;
                                    __arguments[4] = room;
                                    __arguments[5] = true;
                                    {
                                        var ident_3 = __arguments[0];
                                        var secret_3 = __arguments[1];
                                        var domain_3 = __arguments[2];
                                        var application_2 = __arguments[3];
                                        var room_1 = __arguments[4];
                                        var secure = __arguments[5];
                                        //super();
                                        this.fmicelinkxirsysv2ClientInit();
                                        this.setIdent(ident_3);
                                        this.setSecret(secret_3);
                                        this.setDomain(domain_3);
                                        this.setApplication(application_2);
                                        this.setRoom(room_1);
                                        this.setSecure(secure);
                                        this.setEndpoint(fm.icelink.xirsys.v2.Client.getDefaultEndpoint());
                                    }
                                }
                            }
                        }
                        else if (__arguments.length == 4) {
                            var ident = __arguments[0];
                            var secret = __arguments[1];
                            var domain = __arguments[2];
                            var application = __arguments[3];
                            // chained constructor: Client.call(this, ident, secret, domain, application, "default");
                            __arguments = new Array(5);
                            __arguments[0] = ident;
                            __arguments[1] = secret;
                            __arguments[2] = domain;
                            __arguments[3] = application;
                            __arguments[4] = "default";
                            {
                                var ident_4 = __arguments[0];
                                var secret_4 = __arguments[1];
                                var domain_4 = __arguments[2];
                                var application_3 = __arguments[3];
                                var room = __arguments[4];
                                // chained constructor: Client.call(this, ident, secret, domain, application, room, true);
                                __arguments = new Array(6);
                                __arguments[0] = ident_4;
                                __arguments[1] = secret_4;
                                __arguments[2] = domain_4;
                                __arguments[3] = application_3;
                                __arguments[4] = room;
                                __arguments[5] = true;
                                {
                                    var ident_5 = __arguments[0];
                                    var secret_5 = __arguments[1];
                                    var domain_5 = __arguments[2];
                                    var application_4 = __arguments[3];
                                    var room_2 = __arguments[4];
                                    var secure = __arguments[5];
                                    //super();
                                    this.fmicelinkxirsysv2ClientInit();
                                    this.setIdent(ident_5);
                                    this.setSecret(secret_5);
                                    this.setDomain(domain_5);
                                    this.setApplication(application_4);
                                    this.setRoom(room_2);
                                    this.setSecure(secure);
                                    this.setEndpoint(fm.icelink.xirsys.v2.Client.getDefaultEndpoint());
                                }
                            }
                        }
                        else if (__arguments.length == 5) {
                            var ident = __arguments[0];
                            var secret = __arguments[1];
                            var domain = __arguments[2];
                            var application = __arguments[3];
                            var room = __arguments[4];
                            // chained constructor: Client.call(this, ident, secret, domain, application, room, true);
                            __arguments = new Array(6);
                            __arguments[0] = ident;
                            __arguments[1] = secret;
                            __arguments[2] = domain;
                            __arguments[3] = application;
                            __arguments[4] = room;
                            __arguments[5] = true;
                            {
                                var ident_6 = __arguments[0];
                                var secret_6 = __arguments[1];
                                var domain_6 = __arguments[2];
                                var application_5 = __arguments[3];
                                var room_3 = __arguments[4];
                                var secure = __arguments[5];
                                //super();
                                this.fmicelinkxirsysv2ClientInit();
                                this.setIdent(ident_6);
                                this.setSecret(secret_6);
                                this.setDomain(domain_6);
                                this.setApplication(application_5);
                                this.setRoom(room_3);
                                this.setSecure(secure);
                                this.setEndpoint(fm.icelink.xirsys.v2.Client.getDefaultEndpoint());
                            }
                        }
                        else if (__arguments.length == 6) {
                            var ident = __arguments[0];
                            var secret = __arguments[1];
                            var domain = __arguments[2];
                            var application = __arguments[3];
                            var room = __arguments[4];
                            var secure = __arguments[5];
                            //super();
                            this.fmicelinkxirsysv2ClientInit();
                            this.setIdent(ident);
                            this.setSecret(secret);
                            this.setDomain(domain);
                            this.setApplication(application);
                            this.setRoom(room);
                            this.setSecure(secure);
                            this.setEndpoint(fm.icelink.xirsys.v2.Client.getDefaultEndpoint());
                        }
                        else {
                            throw new fm.icelink.Exception('Constructor overload does not exist with specified parameter count/type combination.');
                        }
                    }
                    Client.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v2.Client]';
                    };
                    Client.prototype.fmicelinkxirsysv2ClientInit = function () {
                        this._secure = false;
                    };
                    /**
                    Gets the default HTTP endpoint. Defaults to "https://service.xirsys.com/ice".
                    */
                    Client.getDefaultEndpoint = function () {
                        fm.icelink.xirsys.v2.Client.fmicelinkxirsysv2ClientInitialize();
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v2.Client.fm_icelink_xirsys_v2_Client___defaultEndpoint;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the default HTTP endpoint. Defaults to "https://service.xirsys.com/ice".
                    */
                    Client.setDefaultEndpoint = function (value) {
                        fm.icelink.xirsys.v2.Client.fmicelinkxirsysv2ClientInitialize();
                        if (arguments.length == 1) {
                            fm.icelink.xirsys.v2.Client.fm_icelink_xirsys_v2_Client___defaultEndpoint = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /** @hidden */
                    Client.prototype.doGetIceServers = function (promise) {
                        if (arguments.length == 1) {
                            var str = fm.icelink.HttpTransfer.addQueryToUrl(fm.icelink.HttpTransfer.addQueryToUrl(fm.icelink.HttpTransfer.addQueryToUrl(fm.icelink.HttpTransfer.addQueryToUrl(fm.icelink.HttpTransfer.addQueryToUrl(fm.icelink.HttpTransfer.addQueryToUrl(this.getEndpoint(), "ident", this.getIdent()), "secret", this.getSecret()), "domain", this.getDomain()), "application", this.getApplication()), "room", this.getRoom()), "secure", (this.getSecure() ? "1" : "0"));
                            var requestArgs = new fm.icelink.HttpRequestArgs();
                            requestArgs.setMethod(fm.icelink.HttpMethod.Get);
                            requestArgs.setUrl(str);
                            fm.icelink.HttpTransferFactory.getHttpTransfer().sendTextAsync(requestArgs, function (responseArgs) {
                                var response = fm.icelink.xirsys.v2.IceResponse.fromJson(responseArgs.getTextContent());
                                if ((fm.icelink.Global.equals(response, null))) {
                                    promise.reject(new fm.icelink.Exception("XirSys: null response"));
                                }
                                else {
                                    if ((!fm.icelink.Global.equals(response.getStatus(), 200))) {
                                        promise.reject(new fm.icelink.Exception(fm.icelink.StringExtensions.format("XirSys: {0} {1}", response.getStatus().toString(), response.getError())));
                                    }
                                    else {
                                        var list = new Array();
                                        for (var _i = 0, _a = response.getData().getIceServers(); _i < _a.length; _i++) {
                                            var server = _a[_i];
                                            if (((fm.icelink.Global.equals(server.getUsername(), null)) || (fm.icelink.Global.equals(server.getCredential(), null)))) {
                                                fm.icelink.ArrayExtensions.add(list, new fm.icelink.IceServer(server.getUrl()));
                                            }
                                            else {
                                                fm.icelink.ArrayExtensions.add(list, new fm.icelink.IceServer(server.getUrl(), server.getUsername(), server.getCredential()));
                                            }
                                        }
                                        promise.resolve(fm.icelink.ArrayExtensions.toArray(list));
                                    }
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "application" value. Defaults to "default".
                    */
                    Client.prototype.getApplication = function () {
                        if (arguments.length == 0) {
                            return this._application;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "domain" value.
                    */
                    Client.prototype.getDomain = function () {
                        if (arguments.length == 0) {
                            return this._domain;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the HTTP endpoint.
                    */
                    Client.prototype.getEndpoint = function () {
                        if (arguments.length == 0) {
                            return this._endpoint;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets an array of XirSys ICE servers.
                    */
                    Client.prototype.getIceServers = function () {
                        if (arguments.length == 0) {
                            var promise = new fm.icelink.Promise();
                            this.doGetIceServers(promise);
                            return promise;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "ident" value.
                    */
                    Client.prototype.getIdent = function () {
                        if (arguments.length == 0) {
                            return this._ident;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "room" value. Defaults to "default".
                    */
                    Client.prototype.getRoom = function () {
                        if (arguments.length == 0) {
                            return this._room;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "secret" value.
                    */
                    Client.prototype.getSecret = function () {
                        if (arguments.length == 0) {
                            return this._secret;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "secure" value. Defaults to `true`.
                    */
                    Client.prototype.getSecure = function () {
                        if (arguments.length == 0) {
                            return this._secure;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "application" value. Defaults to "default".
                    */
                    Client.prototype.setApplication = function (value) {
                        if (arguments.length == 1) {
                            this._application = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "domain" value.
                    */
                    Client.prototype.setDomain = function (value) {
                        if (arguments.length == 1) {
                            this._domain = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the HTTP endpoint.
                    */
                    Client.prototype.setEndpoint = function (value) {
                        if (arguments.length == 1) {
                            this._endpoint = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "ident" value.
                    */
                    Client.prototype.setIdent = function (value) {
                        if (arguments.length == 1) {
                            this._ident = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "room" value. Defaults to "default".
                    */
                    Client.prototype.setRoom = function (value) {
                        if (arguments.length == 1) {
                            this._room = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "secret" value.
                    */
                    Client.prototype.setSecret = function (value) {
                        if (arguments.length == 1) {
                            this._secret = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "secure" value. Defaults to `true`.
                    */
                    Client.prototype.setSecure = function (value) {
                        if (arguments.length == 1) {
                            this._secure = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /** @hidden */
                    Client.prototype.toFormData = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.StringExtensions.format("ident={0}&secret={1}&domain={2}&application={3}&room={4}&secure={5}", [this.getIdent(), this.getSecret(), this.getDomain(), this.getApplication(), this.getRoom(), (this.getSecure() ? "1" : "0")]);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /** @hidden */
                    Client.fmicelinkxirsysv2ClientInitialize = function () {
                        if (!fm.icelink.xirsys.v2.Client.__fmicelinkxirsysv2ClientInitialized && !fm.icelink.xirsys.v2.Client.__fmicelinkxirsysv2ClientInitializing) {
                            fm.icelink.xirsys.v2.Client.__fmicelinkxirsysv2ClientInitializing = true;
                            fm.icelink.xirsys.v2.Client.fm_icelink_xirsys_v2_Client___defaultEndpoint = "https://service.xirsys.com/ice";
                            fm.icelink.xirsys.v2.Client.__fmicelinkxirsysv2ClientInitialized = true;
                            fm.icelink.xirsys.v2.Client.__fmicelinkxirsysv2ClientInitializing = false;
                        }
                    };
                    /** @hidden */
                    Client.__fmicelinkxirsysv2ClientInitialized = false;
                    /** @hidden */
                    Client.__fmicelinkxirsysv2ClientInitializing = false;
                    return Client;
                }());
                v2.Client = Client;
            })(v2 = xirsys.v2 || (xirsys.v2 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v2;
            (function (v2) {
                /** @hidden */
                var IceResponse = /** @class */ (function () {
                    function IceResponse() {
                        var __arguments = new Array(arguments.length);
                        for (var __argumentIndex = 0; __argumentIndex < __arguments.length; ++__argumentIndex) {
                            __arguments[__argumentIndex] = arguments[__argumentIndex];
                        }
                        if (__arguments.length == 0) {
                            //super();
                            this.fmicelinkxirsysv2IceResponseInit();
                        }
                        else {
                            throw new fm.icelink.Exception('Constructor overload does not exist with specified parameter count/type combination.');
                        }
                    }
                    IceResponse.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v2.IceResponse]';
                    };
                    IceResponse.prototype.fmicelinkxirsysv2IceResponseInit = function () {
                        this._status = 0;
                    };
                    IceResponse.fromJson = function (iceResponseJson) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.deserializeObject(iceResponseJson, function () {
                                return new fm.icelink.xirsys.v2.IceResponse();
                            }, function (iceResponse, name, valueJson) {
                                var str = name;
                                if ((!fm.icelink.Global.equals(str, null))) {
                                    if (!(fm.icelink.Global.equals(str, "p"))) {
                                        if ((fm.icelink.Global.equals(str, "s"))) {
                                            iceResponse.setStatus(fm.icelink.JsonSerializer.deserializeInteger(valueJson));
                                        }
                                        else {
                                            if ((fm.icelink.Global.equals(str, "e"))) {
                                                iceResponse.setError(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                            }
                                            else {
                                                if ((fm.icelink.Global.equals(str, "d"))) {
                                                    iceResponse.setData(fm.icelink.xirsys.v2.IceResponseData.fromJson(valueJson));
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        iceResponse.setPath(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                    }
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.toJson = function (iceResponse) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObject(iceResponse, function (ir, jsonObject) {
                                if ((!fm.icelink.Global.equals(iceResponse.getPath(), null))) {
                                    jsonObject["p"] = fm.icelink.JsonSerializer.serializeString(iceResponse.getPath());
                                }
                                if ((iceResponse.getStatus() != null)) {
                                    jsonObject["s"] = fm.icelink.JsonSerializer.serializeInteger(iceResponse.getStatus());
                                }
                                if ((!fm.icelink.Global.equals(iceResponse.getError(), null))) {
                                    jsonObject["e"] = fm.icelink.JsonSerializer.serializeString(iceResponse.getError());
                                }
                                if ((!fm.icelink.Global.equals(iceResponse.getData(), null))) {
                                    jsonObject["d"] = fm.icelink.xirsys.v2.IceResponseData.toJson(iceResponse.getData());
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.getData = function () {
                        if (arguments.length == 0) {
                            return this._data;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.getError = function () {
                        if (arguments.length == 0) {
                            return this._error;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.getPath = function () {
                        if (arguments.length == 0) {
                            return this._path;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.getStatus = function () {
                        if (arguments.length == 0) {
                            return this._status;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.setData = function (value) {
                        if (arguments.length == 1) {
                            this._data = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.setError = function (value) {
                        if (arguments.length == 1) {
                            this._error = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.setPath = function (value) {
                        if (arguments.length == 1) {
                            this._path = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.setStatus = function (value) {
                        if (arguments.length == 1) {
                            this._status = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponse.prototype.toJson = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v2.IceResponse.toJson(this);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    return IceResponse;
                }());
                v2.IceResponse = IceResponse;
            })(v2 = xirsys.v2 || (xirsys.v2 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v2;
            (function (v2) {
                /** @hidden */
                var IceResponseData = /** @class */ (function () {
                    function IceResponseData() {
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
                    IceResponseData.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v2.IceResponseData]';
                    };
                    IceResponseData.fromJson = function (iceResponseDataJson) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.deserializeObject(iceResponseDataJson, function () {
                                return new fm.icelink.xirsys.v2.IceResponseData();
                            }, function (iceResponseData, name, valueJson) {
                                var str = name;
                                if (((!fm.icelink.Global.equals(str, null)) && (fm.icelink.Global.equals(str, "iceServers")))) {
                                    iceResponseData.setIceServers(fm.icelink.xirsys.v2.IceServer.fromJsonMultiple(valueJson));
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponseData.toJson = function (iceResponseData) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObject(iceResponseData, function (ird, jsonObject) {
                                if ((!fm.icelink.Global.equals(iceResponseData.getIceServers(), null))) {
                                    jsonObject["iceServers"] = fm.icelink.xirsys.v2.IceServer.toJsonMultiple(iceResponseData.getIceServers());
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponseData.prototype.getIceServers = function () {
                        if (arguments.length == 0) {
                            return this._iceServers;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponseData.prototype.setIceServers = function (value) {
                        if (arguments.length == 1) {
                            this._iceServers = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceResponseData.prototype.toJson = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v2.IceResponseData.toJson(this);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    return IceResponseData;
                }());
                v2.IceResponseData = IceResponseData;
            })(v2 = xirsys.v2 || (xirsys.v2 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));
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

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v2;
            (function (v2) {
                /** @hidden */
                var IceServer = /** @class */ (function (_super) {
                    __extends(IceServer, _super);
                    function IceServer() {
                        var _this = this;
                        var __arguments = new Array(arguments.length);
                        for (var __argumentIndex = 0; __argumentIndex < __arguments.length; ++__argumentIndex) {
                            __arguments[__argumentIndex] = arguments[__argumentIndex];
                        }
                        if (__arguments.length == 0) {
                            _this = _super.call(this) || this;
                        }
                        else {
                            throw new fm.icelink.Exception('Constructor overload does not exist with specified parameter count/type combination.');
                        }
                        return _this;
                    }
                    IceServer.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v2.IceServer]' + ',' + _super.prototype.getTypeString.call(this);
                    };
                    IceServer.fromJson = function (iceServerJson) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.deserializeObjectFast(iceServerJson, function () {
                                return new fm.icelink.xirsys.v2.IceServer();
                            }, function (iceServer, name, valueJson) {
                                var str = name;
                                if ((!fm.icelink.Global.equals(str, null))) {
                                    if (!(fm.icelink.Global.equals(str, "username"))) {
                                        if ((fm.icelink.Global.equals(str, "url"))) {
                                            iceServer.setUrl(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                        }
                                        else {
                                            if ((fm.icelink.Global.equals(str, "credential"))) {
                                                iceServer.setCredential(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                            }
                                        }
                                    }
                                    else {
                                        iceServer.setUsername(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                    }
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.fromJsonMultiple = function (iceServersJson) {
                        if (arguments.length == 1) {
                            var list = fm.icelink.JsonSerializer.deserializeObjectArray(iceServersJson, fm.icelink.xirsys.v2.IceServer.fromJson.bind(fm.icelink.xirsys.v2.IceServer));
                            if ((fm.icelink.Global.equals(list, null))) {
                                return null;
                            }
                            return fm.icelink.ArrayExtensions.toArray(list);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.toJson = function (iceServer) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObjectFast(iceServer, function (iss, jsonObject) {
                                if ((!fm.icelink.Global.equals(iceServer.getUsername(), null))) {
                                    jsonObject["username"] = fm.icelink.JsonSerializer.serializeString(iceServer.getUsername());
                                }
                                if ((!fm.icelink.Global.equals(iceServer.getUrl(), null))) {
                                    jsonObject["url"] = fm.icelink.JsonSerializer.serializeString(iceServer.getUrl());
                                }
                                if ((!fm.icelink.Global.equals(iceServer.getCredential(), null))) {
                                    jsonObject["credential"] = fm.icelink.JsonSerializer.serializeString(iceServer.getCredential());
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.toJsonMultiple = function (iceServers) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObjectArray(iceServers, fm.icelink.xirsys.v2.IceServer.toJson.bind(fm.icelink.xirsys.v2.IceServer));
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.getCredential = function () {
                        if (arguments.length == 0) {
                            return this._credential;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.getUrl = function () {
                        if (arguments.length == 0) {
                            return this._url;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.getUsername = function () {
                        if (arguments.length == 0) {
                            return this._username;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.setCredential = function (value) {
                        if (arguments.length == 1) {
                            this._credential = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.setUrl = function (value) {
                        if (arguments.length == 1) {
                            this._url = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.setUsername = function (value) {
                        if (arguments.length == 1) {
                            this._username = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.toJson = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v2.IceServer.toJson(this);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    return IceServer;
                }(fm.icelink.Dynamic));
                v2.IceServer = IceServer;
            })(v2 = xirsys.v2 || (xirsys.v2 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v3;
            (function (v3) {
                /**
                A XirSys v3 client.
                */
                var Client = /** @class */ (function () {
                    function Client() {
                        fm.icelink.xirsys.v3.Client.fmicelinkxirsysv3ClientInitialize();
                        var __arguments = new Array(arguments.length);
                        for (var __argumentIndex = 0; __argumentIndex < __arguments.length; ++__argumentIndex) {
                            __arguments[__argumentIndex] = arguments[__argumentIndex];
                        }
                        if (__arguments.length == 3) {
                            var ident = __arguments[0];
                            var secret = __arguments[1];
                            var channel = __arguments[2];
                            // chained constructor: Client.call(this, ident, secret, channel, true);
                            __arguments = new Array(4);
                            __arguments[0] = ident;
                            __arguments[1] = secret;
                            __arguments[2] = channel;
                            __arguments[3] = true;
                            {
                                var ident_7 = __arguments[0];
                                var secret_7 = __arguments[1];
                                var channel_1 = __arguments[2];
                                var secure = __arguments[3];
                                //super();
                                this.fmicelinkxirsysv3ClientInit();
                                this.setIdent(ident_7);
                                this.setSecret(secret_7);
                                this.setChannel(channel_1);
                                this.setSecure(secure);
                                this.setEndpoint(fm.icelink.xirsys.v3.Client.getDefaultEndpoint());
                            }
                        }
                        else if (__arguments.length == 4) {
                            var ident = __arguments[0];
                            var secret = __arguments[1];
                            var channel = __arguments[2];
                            var secure = __arguments[3];
                            //super();
                            this.fmicelinkxirsysv3ClientInit();
                            this.setIdent(ident);
                            this.setSecret(secret);
                            this.setChannel(channel);
                            this.setSecure(secure);
                            this.setEndpoint(fm.icelink.xirsys.v3.Client.getDefaultEndpoint());
                        }
                        else {
                            throw new fm.icelink.Exception('Constructor overload does not exist with specified parameter count/type combination.');
                        }
                    }
                    Client.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v3.Client]';
                    };
                    Client.prototype.fmicelinkxirsysv3ClientInit = function () {
                        this._secure = false;
                    };
                    /**
                    Gets the default HTTP endpoint. Defaults to "https://global.xirsys.net/_turn".
                    */
                    Client.getDefaultEndpoint = function () {
                        fm.icelink.xirsys.v3.Client.fmicelinkxirsysv3ClientInitialize();
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v3.Client.fm_icelink_xirsys_v3_Client___defaultEndpoint;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the default HTTP endpoint. Defaults to "https://global.xirsys.net/_turn".
                    */
                    Client.setDefaultEndpoint = function (value) {
                        fm.icelink.xirsys.v3.Client.fmicelinkxirsysv3ClientInitialize();
                        if (arguments.length == 1) {
                            fm.icelink.xirsys.v3.Client.fm_icelink_xirsys_v3_Client___defaultEndpoint = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /** @hidden */
                    Client.prototype.doGetIceServers = function (promise) {
                        if (arguments.length == 1) {
                            var str = (fm.icelink.StringExtensions.endsWith(this.getEndpoint(), "/") ? fm.icelink.StringExtensions.substring(this.getEndpoint(), 0, (fm.icelink.StringExtensions.getLength(this.getEndpoint()) - 1)) : this.getEndpoint());
                            var str2 = fm.icelink.StringExtensions.format("{0}/{1}", str, this.getChannel());
                            var args2 = new fm.icelink.HttpRequestArgs();
                            args2.setMethod(fm.icelink.HttpMethod.Put);
                            args2.setUrl(str2);
                            var requestArgs = args2;
                            requestArgs.getHeaders().set("Authorization", fm.icelink.StringExtensions.format("Basic {0}", fm.icelink.Base64.encode(fm.icelink.Utf8.encode(fm.icelink.StringExtensions.format("{0}:{1}", this.getIdent(), this.getSecret())))));
                            fm.icelink.HttpTransferFactory.getHttpTransfer().sendTextAsync(requestArgs, function (responseArgs) {
                                var response = fm.icelink.xirsys.v3.TurnResponse.fromJson(responseArgs.getTextContent());
                                if ((fm.icelink.Global.equals(response, null))) {
                                    promise.reject(new fm.icelink.Exception("XirSys: null response"));
                                }
                                else {
                                    if ((!fm.icelink.Global.equals(response.getStatus(), fm.icelink.xirsys.v3.TurnResponseStatus.getOK()))) {
                                        promise.reject(new fm.icelink.Exception(fm.icelink.StringExtensions.format("XirSys: {0} {1}", response.getStatus(), fm.icelink.JsonSerializer.deserializeString(response.getValueJson()))));
                                    }
                                    else {
                                        var data = fm.icelink.xirsys.v3.TurnResponseData.fromJson(response.getValueJson());
                                        if ((fm.icelink.Global.equals(data, null))) {
                                            promise.reject(new fm.icelink.Exception(fm.icelink.StringExtensions.format("XirSys: invalid value ({0})", response.getValueJson())));
                                        }
                                        else {
                                            var list = new Array();
                                            for (var _i = 0, _a = data.getIceServers(); _i < _a.length; _i++) {
                                                var server = _a[_i];
                                                if (((fm.icelink.Global.equals(server.getUsername(), null)) || (fm.icelink.Global.equals(server.getCredential(), null)))) {
                                                    fm.icelink.ArrayExtensions.add(list, new fm.icelink.IceServer(server.getUrl()));
                                                }
                                                else {
                                                    fm.icelink.ArrayExtensions.add(list, new fm.icelink.IceServer(server.getUrl(), server.getUsername(), server.getCredential()));
                                                }
                                            }
                                            promise.resolve(fm.icelink.ArrayExtensions.toArray(list));
                                        }
                                    }
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "channel" value.
                    */
                    Client.prototype.getChannel = function () {
                        if (arguments.length == 0) {
                            return this._channel;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the HTTP endpoint.
                    */
                    Client.prototype.getEndpoint = function () {
                        if (arguments.length == 0) {
                            return this._endpoint;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets an array of XirSys ICE servers.
                    */
                    Client.prototype.getIceServers = function () {
                        if (arguments.length == 0) {
                            var promise = new fm.icelink.Promise();
                            this.doGetIceServers(promise);
                            return promise;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "ident" value.
                    */
                    Client.prototype.getIdent = function () {
                        if (arguments.length == 0) {
                            return this._ident;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "secret" value.
                    */
                    Client.prototype.getSecret = function () {
                        if (arguments.length == 0) {
                            return this._secret;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Gets the "secure" value. Defaults to `true`.
                    */
                    Client.prototype.getSecure = function () {
                        if (arguments.length == 0) {
                            return this._secure;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "channel" value.
                    */
                    Client.prototype.setChannel = function (value) {
                        if (arguments.length == 1) {
                            this._channel = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the HTTP endpoint.
                    */
                    Client.prototype.setEndpoint = function (value) {
                        if (arguments.length == 1) {
                            this._endpoint = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "ident" value.
                    */
                    Client.prototype.setIdent = function (value) {
                        if (arguments.length == 1) {
                            this._ident = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "secret" value.
                    */
                    Client.prototype.setSecret = function (value) {
                        if (arguments.length == 1) {
                            this._secret = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /**
                    Sets the "secure" value. Defaults to `true`.
                    */
                    Client.prototype.setSecure = function (value) {
                        if (arguments.length == 1) {
                            this._secure = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    /** @hidden */
                    Client.fmicelinkxirsysv3ClientInitialize = function () {
                        if (!fm.icelink.xirsys.v3.Client.__fmicelinkxirsysv3ClientInitialized && !fm.icelink.xirsys.v3.Client.__fmicelinkxirsysv3ClientInitializing) {
                            fm.icelink.xirsys.v3.Client.__fmicelinkxirsysv3ClientInitializing = true;
                            fm.icelink.xirsys.v3.Client.fm_icelink_xirsys_v3_Client___defaultEndpoint = "https://global.xirsys.net/_turn";
                            fm.icelink.xirsys.v3.Client.__fmicelinkxirsysv3ClientInitialized = true;
                            fm.icelink.xirsys.v3.Client.__fmicelinkxirsysv3ClientInitializing = false;
                        }
                    };
                    /** @hidden */
                    Client.__fmicelinkxirsysv3ClientInitialized = false;
                    /** @hidden */
                    Client.__fmicelinkxirsysv3ClientInitializing = false;
                    return Client;
                }());
                v3.Client = Client;
            })(v3 = xirsys.v3 || (xirsys.v3 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v3;
            (function (v3) {
                /** @hidden */
                var IceServer = /** @class */ (function (_super) {
                    __extends(IceServer, _super);
                    function IceServer() {
                        var _this = this;
                        var __arguments = new Array(arguments.length);
                        for (var __argumentIndex = 0; __argumentIndex < __arguments.length; ++__argumentIndex) {
                            __arguments[__argumentIndex] = arguments[__argumentIndex];
                        }
                        if (__arguments.length == 0) {
                            _this = _super.call(this) || this;
                        }
                        else {
                            throw new fm.icelink.Exception('Constructor overload does not exist with specified parameter count/type combination.');
                        }
                        return _this;
                    }
                    IceServer.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v3.IceServer]' + ',' + _super.prototype.getTypeString.call(this);
                    };
                    IceServer.fromJson = function (iceServerJson) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.deserializeObjectFast(iceServerJson, function () {
                                return new fm.icelink.xirsys.v3.IceServer();
                            }, function (iceServer, name, valueJson) {
                                var str = name;
                                if ((!fm.icelink.Global.equals(str, null))) {
                                    if (!(fm.icelink.Global.equals(str, "username"))) {
                                        if ((fm.icelink.Global.equals(str, "url"))) {
                                            iceServer.setUrl(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                        }
                                        else {
                                            if ((fm.icelink.Global.equals(str, "credential"))) {
                                                iceServer.setCredential(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                            }
                                        }
                                    }
                                    else {
                                        iceServer.setUsername(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                    }
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.fromJsonMultiple = function (iceServersJson) {
                        if (arguments.length == 1) {
                            var list = fm.icelink.JsonSerializer.deserializeObjectArray(iceServersJson, fm.icelink.xirsys.v3.IceServer.fromJson.bind(fm.icelink.xirsys.v3.IceServer));
                            if ((fm.icelink.Global.equals(list, null))) {
                                return null;
                            }
                            return fm.icelink.ArrayExtensions.toArray(list);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.toJson = function (iceServer) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObjectFast(iceServer, function (iss, jsonObject) {
                                if ((!fm.icelink.Global.equals(iceServer.getUsername(), null))) {
                                    jsonObject["username"] = fm.icelink.JsonSerializer.serializeString(iceServer.getUsername());
                                }
                                if ((!fm.icelink.Global.equals(iceServer.getUrl(), null))) {
                                    jsonObject["url"] = fm.icelink.JsonSerializer.serializeString(iceServer.getUrl());
                                }
                                if ((!fm.icelink.Global.equals(iceServer.getCredential(), null))) {
                                    jsonObject["credential"] = fm.icelink.JsonSerializer.serializeString(iceServer.getCredential());
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.toJsonMultiple = function (iceServers) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObjectArray(iceServers, fm.icelink.xirsys.v3.IceServer.toJson.bind(fm.icelink.xirsys.v3.IceServer));
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.getCredential = function () {
                        if (arguments.length == 0) {
                            return this._credential;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.getUrl = function () {
                        if (arguments.length == 0) {
                            return this._url;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.getUsername = function () {
                        if (arguments.length == 0) {
                            return this._username;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.setCredential = function (value) {
                        if (arguments.length == 1) {
                            this._credential = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.setUrl = function (value) {
                        if (arguments.length == 1) {
                            this._url = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.setUsername = function (value) {
                        if (arguments.length == 1) {
                            this._username = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    IceServer.prototype.toJson = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v3.IceServer.toJson(this);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    return IceServer;
                }(fm.icelink.Dynamic));
                v3.IceServer = IceServer;
            })(v3 = xirsys.v3 || (xirsys.v3 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v3;
            (function (v3) {
                /** @hidden */
                var TurnResponse = /** @class */ (function () {
                    function TurnResponse() {
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
                    TurnResponse.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v3.TurnResponse]';
                    };
                    TurnResponse.fromJson = function (turnResponseJson) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.deserializeObject(turnResponseJson, function () {
                                return new fm.icelink.xirsys.v3.TurnResponse();
                            }, function (turnResponse, name, valueJson) {
                                var str = name;
                                if ((!fm.icelink.Global.equals(str, null))) {
                                    if (!(fm.icelink.Global.equals(str, "v"))) {
                                        if ((fm.icelink.Global.equals(str, "s"))) {
                                            turnResponse.setStatus(fm.icelink.JsonSerializer.deserializeString(valueJson));
                                        }
                                    }
                                    else {
                                        turnResponse.setValueJson(fm.icelink.JsonSerializer.deserializeRaw(valueJson));
                                    }
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.toJson = function (turnResponse) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObject(turnResponse, function (gisr, jsonObject) {
                                if ((!fm.icelink.Global.equals(turnResponse.getValueJson(), null))) {
                                    jsonObject["v"] = fm.icelink.JsonSerializer.serializeRaw(turnResponse.getValueJson());
                                }
                                if ((!fm.icelink.Global.equals(turnResponse.getStatus(), null))) {
                                    jsonObject["s"] = fm.icelink.JsonSerializer.serializeString(turnResponse.getStatus());
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.prototype.getStatus = function () {
                        if (arguments.length == 0) {
                            return this._status;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.prototype.getValueJson = function () {
                        if (arguments.length == 0) {
                            return this._valueJson;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.prototype.setStatus = function (value) {
                        if (arguments.length == 1) {
                            this._status = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.prototype.setValueJson = function (value) {
                        if (arguments.length == 1) {
                            this._valueJson = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.prototype.toJson = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v3.TurnResponse.toJson(this);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.prototype.getValue = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.Json.deserialize(this.getValueJson.apply(this, arguments));
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponse.prototype.setValue = function (value) {
                        if (arguments.length == 1) {
                            arguments[arguments.length - 1] = fm.icelink.Json.serialize(arguments[arguments.length - 1]);
                            this.setValueJson.apply(this, arguments);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    return TurnResponse;
                }());
                v3.TurnResponse = TurnResponse;
            })(v3 = xirsys.v3 || (xirsys.v3 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v3;
            (function (v3) {
                /** @hidden */
                var TurnResponseData = /** @class */ (function () {
                    function TurnResponseData() {
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
                    TurnResponseData.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v3.TurnResponseData]';
                    };
                    TurnResponseData.fromJson = function (turnResponseDataJson) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.deserializeObject(turnResponseDataJson, function () {
                                return new fm.icelink.xirsys.v3.TurnResponseData();
                            }, function (turnResponseData, name, valueJson) {
                                var str = name;
                                if (((!fm.icelink.Global.equals(str, null)) && (fm.icelink.Global.equals(str, "iceServers")))) {
                                    turnResponseData.setIceServers(fm.icelink.xirsys.v3.IceServer.fromJsonMultiple(valueJson));
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponseData.toJson = function (turnResponseData) {
                        if (arguments.length == 1) {
                            return fm.icelink.JsonSerializer.serializeObject(turnResponseData, function (ird, jsonObject) {
                                if ((!fm.icelink.Global.equals(turnResponseData.getIceServers(), null))) {
                                    jsonObject["iceServers"] = fm.icelink.xirsys.v3.IceServer.toJsonMultiple(turnResponseData.getIceServers());
                                }
                            });
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponseData.prototype.getIceServers = function () {
                        if (arguments.length == 0) {
                            return this._iceServers;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponseData.prototype.setIceServers = function (value) {
                        if (arguments.length == 1) {
                            this._iceServers = value;
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponseData.prototype.toJson = function () {
                        if (arguments.length == 0) {
                            return fm.icelink.xirsys.v3.TurnResponseData.toJson(this);
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    return TurnResponseData;
                }());
                v3.TurnResponseData = TurnResponseData;
            })(v3 = xirsys.v3 || (xirsys.v3 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v3;
            (function (v3) {
                /** @hidden */
                var TurnResponseStatus = /** @class */ (function () {
                    function TurnResponseStatus() {
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
                    TurnResponseStatus.prototype.getTypeString = function () {
                        return '[fm.icelink.xirsys.v3.TurnResponseStatus]';
                    };
                    TurnResponseStatus.getError = function () {
                        if (arguments.length == 0) {
                            return "error";
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    TurnResponseStatus.getOK = function () {
                        if (arguments.length == 0) {
                            return "ok";
                        }
                        else {
                            throw new fm.icelink.Exception('Method overload does not exist with specified parameter count/type combination.');
                        }
                    };
                    return TurnResponseStatus;
                }());
                v3.TurnResponseStatus = TurnResponseStatus;
            })(v3 = xirsys.v3 || (xirsys.v3 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));
/// <reference path="v2/Client.ts" />

(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v2;
            (function (v2) {
                fm.icelink.xirsys.v2.Client.fmicelinkxirsysv2ClientInitialize();
            })(v2 = xirsys.v2 || (xirsys.v2 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));
/// <reference path="v3/Client.ts" />
(function (fm) {
    var icelink;
    (function (icelink) {
        var xirsys;
        (function (xirsys) {
            var v3;
            (function (v3) {
                fm.icelink.xirsys.v3.Client.fmicelinkxirsysv3ClientInitialize();
            })(v3 = xirsys.v3 || (xirsys.v3 = {}));
        })(xirsys = icelink.xirsys || (icelink.xirsys = {}));
    })(icelink = fm.icelink || (fm.icelink = {}));
})(fm || (fm = {}));
return fm.icelink.xirsys;
}));
