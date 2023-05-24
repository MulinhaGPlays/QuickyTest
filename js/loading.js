var loadingScreen = document.getElementById('loading-screen');
var content = document.getElementById('content');

function removeLoadingScreen() { 
    setInterval(() => {
        loadingScreen.style.display = 'none';
    }, 1900)
}

window.addEventListener('load', removeLoadingScreen);