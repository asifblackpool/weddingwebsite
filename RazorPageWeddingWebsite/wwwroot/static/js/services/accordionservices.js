const accordionservices = (function () {
    // Private variables
    let accordionItems = [];

    // Private methods
    function closeAllItems() {
        accordionItems.forEach(item => {
            item.classList.remove('active');
            const content = item.querySelector('.accordion-content');
            if (content) {
                content.style.display = 'none';
            }
        });
    }

    function toggleItem(item) {
        const isActive = item.classList.contains('active');
        const content = item.querySelector('.accordion-content');

        closeAllItems();

        if (!isActive && content) {
            item.classList.add('active');
            //content.style.maxHeight = content.scrollHeight + 'px';
            content.style.display = 'block';
        }
    }

    function initialize(selector = '.accordion-item') {
        accordionItems = document.querySelectorAll(selector);

        accordionItems.forEach(item => {
            const header = item.querySelector('.accordion-header');
            if (header) {
                header.addEventListener('click', () => toggleItem(item));
                // Initialize all as closed
                const content = item.querySelector('.accordion-content');
                if (content) {
                    content.style.maxHeight = null;
                    content.style.display = 'none'; // Changed to display none
                }
            }
        });

        // Removed the "open first item by default" code
    }

    // Public API
    return {
        init: function (selector) {
            initialize(selector);
        },
        openItem: function (index) {
            if (accordionItems[index]) {
                toggleItem(accordionItems[index]);
            }
        },
        closeAll: function () {
            closeAllItems();
        },
        refresh: function () {
            accordionItems.forEach(item => {
                if (item.classList.contains('active')) {
                    const content = item.querySelector('.accordion-content');
                    if (content) {
                        content.style.maxHeight = content.scrollHeight + 'px';
                    }
                }
            });
        }
    };
})();



