/**
 * EqualHeightService - Makes elements with the same class equal height
 * @param {string} className - Class name to target (default: 'stories-information')
 * @param {number} breakpoint - Mobile breakpoint in pixels (default: 768)
 */
const equalheightservices = (function () {
    // Private variables
    let className = 'stories-information';
    let breakpoint = 768;
    let elements = [];
    let resizeTimer;
    let observer;

    // Main function to adjust heights
    function adjustHeights() {
        // Mobile check
        if (window.innerWidth < breakpoint) {
            resetHeights();
            return;
        }

        let maxHeight = 0;

        // Reset and find max height
        elements.forEach(el => {
            el.style.height = '';
            maxHeight = Math.max(maxHeight, el.offsetHeight);
        });

        // Apply to all elements
        elements.forEach(el => {
            el.style.height = `${maxHeight}px`;

        });
    }

    // Reset heights to auto
    function resetHeights() {
        elements.forEach(el => {
            el.style.height = '';
        });
    }

    // Initialize the service
    function init(config = {}) {
        // Apply configuration

        console.log('equal height services called init()');
        className = config.className || className;
        breakpoint = config.breakpoint || breakpoint;

        // Get elements
        elements = Array.from(document.querySelectorAll(`.${className}`));
        if (elements.length < 2) return;

        // Initial adjustment
        adjustHeights();

        // Set up event listeners
        window.addEventListener('resize', () => {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(adjustHeights, 100);
        });

        // Watch for DOM changes
        if (typeof MutationObserver !== 'undefined') {
            observer = new MutationObserver(adjustHeights);
            observer.observe(document.body, {
                subtree: true,
                childList: true,
                attributes: true,
                attributeFilter: ['class']
            });
        }

        return {
            refresh: adjustHeights,
            destroy: cleanup
        };
    }

    // Clean up event listeners
    function cleanup() {
        resetHeights();
        window.removeEventListener('resize', adjustHeights);
        if (observer) observer.disconnect();
    }

    // Public API
    return {
        init: init
    };
})();

// Usage (default)
//document.addEventListener('DOMContentLoaded', function () {
    //equalheightservices.init();

    // Alternative usage with config:
    // equalheightservices.init({
    //   className: 'my-custom-class',
    //   breakpoint: 1024
    // });

    //const heightService = equalheightservices.init();
    // Later...

    //heightService.refresh(); // Recalculate heights

    // When done...
    //heightService.destroy();
//});