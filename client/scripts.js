const CLIENT_BASE = `${window.location.protocol}//${window.location.hostname}:8080`;
const API_BASE = `${window.location.protocol}//${window.location.hostname}:8081`;

window.onload = () => {
  const page = document.getElementById('page');
  if (page.content === 'results') {
    getResults();
  }
};

const tags = new Set();

function tag(value) {
  if (tags.has(value)) {
    tags.remove(value);
  } else {
    tags.add(value);
  }
  updateSearchUrl();
}

function updateSearchUrl() {
  const searchBar = document.getElementById('search-bar');
  const resultsUrl = new URL(`${CLIENT_BASE}/results.html`);
  resultsUrl.searchParams.append('search', searchBar.value);
  tags.forEach(tag => resultsUrl.searchParams.append('tag', tag));
  document.getElementById('search-results').href = resultsUrl.href;
}

function getResults() {
  const currentUrl = new URL(location.href);
  const apiUrl = new URL(`${API_BASE}/api/recipe`);
  apiUrl.searchParams = currentUrl.searchParams;
  fetch(apiUrl).then(result => result.json()).then(data => {
    const results = document.getElementById('results-list');
    data.forEach(item => {
      const li = document.createElement('li');
      li.textContent = item.name;
      results.append(li);
    });
  });
}
