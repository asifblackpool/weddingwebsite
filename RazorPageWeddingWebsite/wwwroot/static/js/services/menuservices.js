
const menuservices = (function () {
    // private variables
    const selectors = {
        menuToggle: ".mobile-menu-toggle",
        navList: ".nav-list",
        submenuLinks: ".nav-item.has-panel > span.caret"
    };

    _menuCaret = null;
    _munuSubParent = null;
    _parent = null;
    _nearestLink = null;
    _main = null;
    _homeTitle = null;


    var setupCaret = function (caret) {

    };


    var closeSubMenu = function () {
        if (_parent && _parent.classList.contains("open")) {
            _parent.classList.toggle("open");
            if (_nearestLink) { _nearestLink.style.color = ""; }
        }
    };

    var initialise = function () {
        const menuToggle = document.querySelector(selectors.menuToggle);
        const navList = document.querySelector(selectors.navList);


        if (!menuToggle || !navList) return;

        const menuCaret = menuToggle.querySelector(".mobile-caret");
        //menuCaret.textContent = "▼"; // caret swap

        // Main mobile menu toggle
        menuToggle.addEventListener("click", () => {
            const isOpen = navList.style.display === "flex";
            navList.style.display = isOpen ? "none" : "flex";
            menuToggle.classList.toggle("active", !isOpen);

            _main = document.querySelector("main");
            _homeTitle = document.querySelector("h1.home");

            if (!isOpen) {
                //we are opening the menu
                menuToggle.classList.add("active");
                _main.style.opacity = "0.1";
                _homeTitle.style.opacity = "0.3";

                navList.style.paddingBottom = (_main.offsetHeight - navList.offsetHeight) + "px";
            }
            else {
                //We are closing the menu
                menuToggle.classList.remove("active");
                _main.style.opacity = "1";
                _homeTitle.style.opacity = "1";
                navList.style.paddingBottom = "0px";
                closeSubMenu();
            }

        });

        // Submenu toggles
        document.querySelectorAll(selectors.submenuLinks).forEach(link => {
            link.addEventListener("click", (e) => {
                if (window.innerWidth <= 576) {
                    e.preventDefault();
                    _parent = link.parentElement;

                    const caret = link.querySelector(".caret");
                    _parent.classList.toggle("open");

                    let navItem = link.closest(".nav-item");    // get the parent li
                    _nearestLink = navItem.querySelector("a");
                    if (_parent.classList.contains("open")) {
                        _nearestLink.style.color = "#dd3394";
                    } else {
                        _nearestLink.style.color = "";
                    }
                }
            });
        });
    };

    // public API
    return {
        init: function () {
            console.log('menuservices init called');
            initialise();
        }
    };
})();


