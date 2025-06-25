// Local state shape for the form fields
export interface PatientForm {
  nhsNumber: string;
  surname: string;
  dateOfBirth: string;
}

// Props passed to the component
export interface ValidateFormProps {
  onValidated: (valid: boolean, age: number | null) => void;
}

// Add ValidationResponse here
export interface ValidationResponse {
  age: number;
  message: string;
}