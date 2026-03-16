const API_BASE = `${window.location.protocol}//${window.location.hostname}:8081`;

function search() {
  const currentUrl = new URL(location.href);
  const apiUrl = new URL(`${API_BASE}/api/recipe`);
  apiUrl.searchParams = currentUrl.searchParams;
  fetch(apiUrl).then(result => result.json()).then(data => {
    const recipes = document.getElementById('recipes');
    recipes.innerText = JSON.stringify(data);
  });
}
