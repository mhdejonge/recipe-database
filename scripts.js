const SITE_BASE_URL = window.location.origin + '/recipe-database';

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
  const searchText = document.getElementById('search-bar')?.value?.trim();
  const resultsUrl = new URL(`${SITE_BASE_URL}/results.html`);
  if (searchText) {
    resultsUrl.searchParams.append('search', searchText);
  }
  tags.forEach(tag => resultsUrl.searchParams.append('tags', tag));
  document.getElementById('search-results').href = resultsUrl.href;
}

function searchRecipes(recipes) {
  const currentUrl = new URL(location.href);
  const search = currentUrl.searchParams.get('search');
  const tags = currentUrl.searchParams.getAll('tags');
  if (!search && !tags.length) {
    return recipes;
  }
  if (search) {
    recipes = recipes.filter(recipe => recipe.name.toLowerCase().search(new RegExp(`.*${search}.*`, 'i')));
  }
  if (tags.length) {
    recipes = recipes.filter(recipe => recipe.tags.some(tag => tags.includes(tag)));
  }
  return recipes;
}

function getResults() {
  fetch(`${SITE_BASE_URL}/recipes.json`).then(result => result.json()).then(recipes => {
    recipes = searchRecipes(recipes);
    const results = document.getElementById('results-list');
    if (recipes.length) {
      recipes.forEach(item => {
        const li = document.createElement('li');
        li.textContent = item.name;
        results.append(li);
      });
    } else {
      const li = document.createElement('li');
      li.textContent = 'No results found';
      results.append(li);
    }
  });
}
