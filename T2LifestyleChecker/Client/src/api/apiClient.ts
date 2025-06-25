import axios from 'axios';

// Create a pre-configured Axios instance for API calls
const api = axios.create({
  // Base URL for all HTTP requests made using this instance
  // This saves you from repeating the full URL every time you call api.get or api.post
  baseURL: 'http://localhost:5206/api/LifestyleChecker',
});

export default api;
