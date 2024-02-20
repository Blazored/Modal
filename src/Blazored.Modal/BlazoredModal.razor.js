const el = document.body;
const computedBodyStyle = getComputedStyle(el);
const originalProps = { overflow: computedBodyStyle.overflow, paddingRight: computedBodyStyle.paddingRight };
const getScrollBarWidth = () => {
    let el = document.createElement("div");
    el.style.cssText = "overflow:scroll; visibility:hidden; position:absolute;";
    document.body.appendChild(el);
    let width = el.offsetWidth - el.clientWidth;
    el.remove();
    return width;
}
const isScrollbarPresent = () => {
    const beforeScrollbarHidden = document.body.clientWidth;
    const overflowState = document.body?.style.overflow;
    document.body.style.overflow = 'hidden';
    const afterScrollbarHidden = document.body.clientWidth;
    document.body.style.overflow = overflowState;
    return beforeScrollbarHidden !== afterScrollbarHidden;
};

export function setBodyStyle() {
    if (isScrollbarPresent()) {
        el.style.paddingRight = `${getScrollBarWidth()}px`;
    }
    
    el.style.overflow = 'hidden';
}

export function removeBodyStyle() {
    el.style.overflow = originalProps.overflow || 'auto';
    el.style.paddingRight = originalProps.paddingRight;
}