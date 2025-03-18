document.querySelector("#friendlist_button").addEventListener("click",async function () {
    var response = await fetch("friends-list")
    var body = await response.text();
    document.querySelector(".friends_list").innerHTML = body;
});