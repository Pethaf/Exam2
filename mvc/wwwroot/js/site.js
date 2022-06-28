// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", () => {
    if (localStorage["theme"] === undefined || localStorage["theme"] === "light") {
        localStorage["theme"] = "dark";
    }
    else {
        localStorage["theme"] = "light"
    }
    changeMode();
    document.querySelector(".fa").addEventListener("click", () => {
        changeMode();
    })
    let visibilityP = document.querySelector(".toggle-comment-form-visibility")
    if (visibilityP !== null) {
        visibilityP.addEventListener("click", () => {
            document.querySelector(".comment-form").classList.toggle("hidden");
            if (!document.querySelector(".comment-form").classList.contains("hidden")) {
                document.querySelector(".comment-form input").focus();
            }
        })
    }
    if ([...document.querySelectorAll(".comment-form .text-danger")].some(t => t.innerHTML.length !== 0)) {
        document.querySelector(".comment-form").classList.remove("hidden");
        document.querySelector(".comment-form input").focus();
    }
    [...document.querySelectorAll(".article-detail-comment")].forEach((e, idx) => { if (idx % 2 === 1) { e.classList.add("highlight") } })
    const callback = (elements) => {
        elements.forEach(element => {
            if (element.isIntersecting) {
                let animation = element.target.dataset.animate;
                element.target.style.animation = animation;
            }
            else {
                element.target.style.animation = "none";
            }

        })
    }
    let observer = new IntersectionObserver(callback)
    const animationItems = document.querySelectorAll(".animate");
    animationItems.forEach(item => {
        observer.observe(item)
    })
    let infoButton = document.querySelector(".fa-circle-info");
    if (infoButton !== null) {
        infoButton.addEventListener("click", (event) => {
            document.querySelector("#popup").classList.remove("hidden");
        })
    }
    let closeButton = document.querySelector(".popup-head .fa-xmark")
    if(closeButton){
        closeButton.addEventListener("click",()=>{
        document.querySelector("#popup").classList.add("hidden");
    })}
})


const changeMode = () => {
    const oldTheme = localStorage["theme"];
    if (oldTheme === "dark") {
        localStorage["theme"] = "light";
        document.querySelector(".theme-control .fa").className = "fa fa-toggle-off";
    }
    else {
        localStorage["theme"] = "dark";
        document.querySelector(".theme-control .fa").className = "fa fa-toggle-on";
    }
    const elementsToChange = ["body",".pinned-article-link", 
                              "footer","#latest-news-article-container",
                              ".article-view-article-container",".article-view-comment-container",
                              ".article-detail-comment",".author-panel"];
    elementsToChange.forEach(elem => {
        if(elem !==undefined){
        document.querySelectorAll(elem).forEach((elem2) => {
            if (oldTheme === "dark") {
                elem2.classList.remove("dark-mode");
            }
            else {
                elem2.classList.add("dark-mode");
            }

        })
    }
    })
}
