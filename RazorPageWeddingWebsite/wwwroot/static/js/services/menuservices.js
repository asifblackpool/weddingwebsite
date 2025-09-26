
 const menuservices = (function () {
     // private variables
      const selectors = {
        menuToggle: ".mobile-menu-toggle",
        navList: ".nav-list",
        submenuLinks: ".nav-item.has-panel > a"
     };

     _menuCaret = null;
     _munuSubParent = null;

     var setupCaret = function (caret) {

     };

     var closeSubMenu = function () {
         if (_parent.classList.contains("open")) {
             _parent.classList.toggle("open");
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
             if (!isOpen) {
                 menuToggle.classList.add("active");
             }
             else {
                 menuToggle.classList.remove("active");
                 closeSubMenu();
             }
             //menuCaret.textContent = isOpen ? "▼" : "✕"; // caret swap
         });

         // Submenu toggles
         document.querySelectorAll(selectors.submenuLinks).forEach(link => {
             link.addEventListener("click", (e) => {
                 if (window.innerWidth <= 576) {
                     e.preventDefault();
                     _parent = link.parentElement;
                     const caret = link.querySelector(".caret");
                     _parent.classList.toggle("open");
                     //caret.textContent = parent.classList.contains("open") ? "✕" : "▼";
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


