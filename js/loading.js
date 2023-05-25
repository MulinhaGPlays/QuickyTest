var content = document.getElementById('content');
var loadingScreen = document.getElementById('loading-screen');

function removeLoadingScreen() { 
    loadingScreen.classList.add('finalizado');
    setInterval(() => {
        loadingScreen.style.display = 'none';
        content.style.display = 'block';
    }, 1900)
}

window.addEventListener('load', removeLoadingScreen);