import React, { useState } from 'react';
import ValidateForm from './components/ValidateForm';
import LifestyleForm from './components/LifestyleForm';

/**
 * Main application component that controls the overall flow of the Health Checker app.
 * 
 * It manages user validation state and age, and conditionally renders the 
 * patient validation form or the lifestyle scoring form based on validation status.
 */
const App: React.FC = () => {
  // State to track whether the user has been successfully validated
  const [isValidated, setIsValidated] = useState<boolean>(false);
  
  // State to store the validated user's age; null means no age available yet
  const [age, setAge] = useState<number | null>(null);

  /**
   * Callback handler invoked by ValidateForm after attempting to validate a user.
   * Updates validation status and age in the state.
   * 
   * @param valid - boolean indicating if validation succeeded
   * @param ageValue - the user's age if validated; null otherwise
   */
  const handleValidation = (valid: boolean, ageValue: number | null): void => {
    setIsValidated(valid);
    // Only set age if valid and ageValue is not null, otherwise clear it
    setAge(valid && ageValue !== null ? ageValue : null);
  };

  /**
   * Handler for resetting the validation state and age, effectively
   * returning the app to the initial "validate patient" step.
   */
  const handleBack = () => {
    setIsValidated(false);
    setAge(null);
  };

  return (
    <div className="app-container">
      <h1>Health Checker</h1>

      {/* 
        Conditionally render ValidateForm when user is not validated yet. 
        Pass the handleValidation function as a prop for callback.
      */}
      {!isValidated && <ValidateForm onValidated={handleValidation} />}

      {/*
        Once validated and age is available, show a Back button and the LifestyleForm.
        The Back button allows user to reset and validate again.
      */}
      {isValidated && age !== null && (
        <>
          <button onClick={handleBack} style={{ marginBottom: '1rem' }}>
            &larr; Back to Validate
          </button>
          <LifestyleForm age={age} />
        </>
      )}
    </div>
  );
};

export default App;
