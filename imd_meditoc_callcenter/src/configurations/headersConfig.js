const appKey = {
    name: "AppKey",
    value: "qSVBJIQpOqtp0UfwzwX1ER6fNYR8YiPU/bw5CdEqYqk=",
};

const appToken = {
    name: "AppToken",
    value: "Xx3ePv63cUTg77QPATmztJ3J8cdO1riA7g+lVRzOzhfnl9FnaVT1O2YIv8YCTVRZ",
};

const contentType = {
    name: "Content-Type",
    value: "application/json",
};

const MeditocHeaders = {
    [appKey.name]: appKey.value,
    [appToken.name]: appToken.value,
};

const MeditocHeadersCT = {
    [appKey.name]: appKey.value,
    [appToken.name]: appToken.value,
    [contentType.name]: contentType.value,
};

export { MeditocHeaders, MeditocHeadersCT };
