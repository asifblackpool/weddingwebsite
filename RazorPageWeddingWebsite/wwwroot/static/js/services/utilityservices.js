const utilityservices = (function () {
    // private function
    function replaceCommasInParagraph(selector) {
        const p = document.querySelector(selector);
        if (!p) return;

        const text = p.textContent || "";
        const updated = text.replace(/,\s*/g, "<br/>"); // replace commas with <br/>
        p.innerHTML = updated;
    }

    // public API
    return {
        replaceCommasInParagraph: replaceCommasInParagraph
    };
})();
