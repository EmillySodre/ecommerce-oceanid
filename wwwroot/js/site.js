
/*MENU CADASTRO*/
function menuCad() {
    const menu = document.getElementById("menu-cad");
    menu.classList.toggle("active");
}

//MENUU SACOOLAA
function abrirSacola() {
    document.getElementById('menu-sac').classList.add('aberta');
    document.getElementById('sacola-overlay').style.display = 'block';
}

function fecharSacola() {
    document.getElementById('menu-sac').classList.remove('aberta');
    document.getElementById('sacola-overlay').style.display = 'none';
}

// FAVORITOS
function toggleFavorito(element) {
    event.preventDefault(); // evita que o link recarregue a página

    var img = element.querySelector('img');

    // Verifica a imagem atual e alterna para a outra
    if (img.src.includes('~/img/favs-icon.png')) {
        img.src = '~/img/favsP-icon.png';
    } else {
        img.src = '~/img/favs-icon.png';
    }
}

//SENHAAA VISIVELL
function toggleSenha() {
    var input = document.getElementById("senha");
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}

//MENUUU HAMBURGUERR MOBILEE

function toggleMobileMenu() {
    var menu = document.getElementById("mobileMenu");
    menu.classList.toggle("show");
}
function toggleMobileMenu() {
    var menu = document.getElementById("mobileMenu");
    var body = document.body;

    if (menu.classList.contains("show")) {
        menu.classList.remove("show");
        body.style.overflow = "auto"; // libera scroll
    } else {
        menu.classList.add("show");
        body.style.overflow = "hidden"; // trava scroll
    }
}
