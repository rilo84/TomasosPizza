toogleDiv = (item) => {
    $(item).toggle();
}

addItem = (div) => {
    let val = $("#listIngredients").val();
    let text = $("#listIngredients :selected").text();
    $(div).append(`<option value="${val}">${text}</option>`);                                             
}

removeItem = (div) => {
    $(`${div} :selected`).remove();
}