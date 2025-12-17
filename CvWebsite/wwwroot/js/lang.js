export function getLang() {
    return localStorage.getItem("lang") || "nl";
}
export function setLang(lang) {
    localStorage.setItem("lang", lang);
}