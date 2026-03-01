(function (window) {
  window.__env = window.__env || {};

  // Base API URL (change per environment)
  window.__env.baseUrl = 'https://localhost:5001/api';

  // Other environment-specific flags
  window.__env.enableDebug = true;
})(this);
