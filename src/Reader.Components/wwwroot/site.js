function addToLocalStorage(key, value) { localStorage[key] = value; }
function readLocalStorage(key) { return localStorage[key]; }

function scrollContentTo(percentage) {
  // Convert percentage to pixels value
  const maxPosition = document.body.clientHeight;
  const yValue = maxPosition * percentage;
  console.log(percentage);
  console.log(yValue);
  window.scrollTo({
    top: yValue,
    left: 0,
    behavior: "smooth",
  });
}

let OldScroll = 0;

window.addEventListener("scrollend", (event) => {
  if (window.scrollInfoService != null) {
    OldScroll = this.scrollY;
    const scrollPosition = document.body.getBoundingClientRect().top;
    const maxPosition = document.body.clientHeight;
    const scrollPercent = scrollPosition / maxPosition;

    window.scrollInfoService.invokeMethodAsync('OnScrollEnd', scrollPercent);
  }
});

window.addEventListener("scroll", (event) => {
  const isScrollDirectionUp = OldScroll > this.scrollY
  OldScroll = this.scrollY;

  window.scrollInfoService.invokeMethodAsync('OnScroll', isScrollDirectionUp);
});

window.RegisterScrollInfoService = (scrollInfoService) => {
  window.scrollInfoService = scrollInfoService;
}
