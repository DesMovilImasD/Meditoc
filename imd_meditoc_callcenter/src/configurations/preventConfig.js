const funcPrevent = (e) => {
    if (e !== undefined) {
        if (typeof e.preventDefault === "function") {
            e.preventDefault();
        }
    }
};

const blurPrevent = () => {
    if (typeof document.activeElement.blur === "function") {
        document.activeElement.blur();
    }
};

export { funcPrevent, blurPrevent };
