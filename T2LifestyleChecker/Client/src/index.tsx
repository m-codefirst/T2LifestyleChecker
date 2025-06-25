import React from 'react';
import ReactDOM from 'react-dom/client'; 
import './styles.css'; 
import App from './App'; // Import the main App component
import { BrowserRouter as Router } from 'react-router-dom'; // React Router for client-side routing

/**
 * Entry point for the React application.
 */

// Create a React root and attach it to the DOM element with id 'root'
const root = ReactDOM.createRoot(document.getElementById('root')!);

// Render the React app inside the root element
root.render(
  // React.StrictMode activates additional checks and warnings for potential problems
  <React.StrictMode>
    {/* Router enables routing/navigation inside the app */}
    <Router>
      <App />
    </Router>
  </React.StrictMode>
);
