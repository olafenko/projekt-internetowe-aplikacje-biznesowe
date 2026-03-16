// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const hamburger = document.getElementById("hamburger");

const menu = document.getElementById("sidebarMenu");


hamburger.addEventListener("click", () => {

    menu.classList.toggle("sidebar-menu-open");

})