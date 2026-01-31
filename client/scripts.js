const API_BASE = `${window.location.protocol}//${window.location.hostname}:8081`;
fetch(`${API_BASE}/api/recipe`).then(result => result.json()).then(console.log);
