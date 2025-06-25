import React, { useState } from 'react';
import api from '../api/apiClient'; // Axios instance for API calls

// Import TypeScript types for props and data models
import { PatientForm, ValidateFormProps, ValidationResponse } from '../types/validateFormTypes';

/**
 * ValidateForm component:
 * 
 * Renders a form to collect patient details (NHS Number, Surname, DOB) for validation.
 * On submission, it sends data to an API endpoint to verify the patient,
 * then notifies the parent component of validation status and age.
 */
const ValidateForm: React.FC<ValidateFormProps> = ({ onValidated }) => {
  // Local state to track form input values for patient details
  const [form, setForm] = useState<PatientForm>({
    nhsNumber: '',
    surname: '',
    dateOfBirth: ''
  });

  // State to display messages (success/error) to the user
  const [message, setMessage] = useState<string>('');

  /**
   * Event handler to update form state when an input changes.
   * Uses the input's `name` attribute to dynamically update the corresponding field.
   */
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  /**
   * Handles form submission:
   * - Prevents default page reload behavior.
   * - Sends form data to the `/validate` API endpoint.
   * - Based on response:
   *   - Shows the message returned from the API.
   *   - Calls `onValidated` callback with success flag and age if valid.
   *   - Otherwise, notifies validation failure.
   * - Handles and logs any errors during API call.
   */
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      // POST patient info to validate API; expect ValidationResponse shape
      const response = await api.post<ValidationResponse>('/validate', form);
      const data = response.data;

      // Show message returned from API (could be success or failure info)
      setMessage(data.message);

      // Validate response: status 200, age present and over 16
      if (response.status === 200 && typeof data.age === 'number' && data.age > 16) {
        // Notify parent component that validation succeeded and pass age
        onValidated(true, data.age);
      } else {
        // Notify parent component validation failed
        onValidated(false, null);
      }

    } catch (err) {
      console.error(err);
      setMessage('Your details could not be found');
      onValidated(false, null);
    }
  };

  return (
    <div>
      <h2>Validate Patient</h2>
      <form onSubmit={handleSubmit}>
        {/* NHS Number input */}
        <div>
          <label>NHS Number:</label>
          <input
            type="text"
            name="nhsNumber"
            value={form.nhsNumber}
            onChange={handleChange}
            required
          />
        </div>

        {/* Surname input */}
        <div>
          <label>Surname:</label>
          <input
            type="text"
            name="surname"
            value={form.surname}
            onChange={handleChange}
            required
          />
        </div>

        {/* Date of Birth input */}
        <div>
          <label>Date of Birth:</label>
          <input
            type="date"
            name="dateOfBirth"
            value={form.dateOfBirth}
            onChange={handleChange}
            required
          />
        </div>

        {/* Submit button */}
        <button type="submit">Validate</button>
      </form>

      {/* Conditionally render message if present */}
      {message && <p><strong>{message}</strong></p>}
    </div>
  );
};

export default ValidateForm;
