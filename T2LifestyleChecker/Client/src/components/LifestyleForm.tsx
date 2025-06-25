import '../LifestyleForm.css'; // Importing component-specific styles
import React, { useEffect, useState, ChangeEvent, FormEvent } from 'react';
import api from '../api/apiClient'; 

// Importing relevant TypeScript types for strong typing
import { Question, Answer, LifestyleFormProps, ScoreResponse } from '../types/lifestyleFormTypes';

/**
 * LifestyleForm component:
 * 
 * Displays a list of lifestyle questions fetched from the server.
 */
const LifestyleForm: React.FC<LifestyleFormProps> = ({ age }) => {
  // State to hold questions fetched from the server
  const [questions, setQuestions] = useState<Question[]>([]);

  // Form state holding answers keyed by question id and age as a string
  const [form, setForm] = useState<Record<string, string | boolean>>({ age: age.toString() });

  // State to hold the scoring response from the API
  const [result, setResult] = useState<ScoreResponse | null>(null);

  /**
   * useEffect hook runs whenever 'age' prop changes:
   * - Updates the 'age' in the form state.
   * - Fetches the list of lifestyle questions from the API.
   * - Initializes all question answers to false (unchecked).
   */
  useEffect(() => {
    // Update age in the form state as string
    setForm((prev) => ({ ...prev, age: age.toString() }));

    // Fetch questions async function
    const fetchQuestions = async () => {
      try {
        const response = await api.get('/questions');

        // Extract questions array from response
        const questionsArray: Question[] = Object.values(response.data.questions);
        setQuestions(questionsArray);

        // Initialize answers: set each question checkbox answer to false
        const initialAnswers: Record<string, boolean> = {};
        questionsArray.forEach((q) => {
          initialAnswers[`q${q.id}`] = false;
        });

        // Merge initialized answers into form state
        setForm((prev) => ({ ...prev, ...initialAnswers }));
      } catch (err) {
        console.error('Failed to load questions:', err);
      }
    };

    fetchQuestions();
  }, [age]);

  /**
   * Handles changes to form inputs (checkbox toggles):
   * - Updates form state dynamically based on input name and type.
   */
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, type, checked, value } = e.target;
    setForm({
      ...form,
      [name]: type === 'checkbox' ? checked : value,
    });
  };

  /**
   * Handles form submission:
   * - Prevents default form behavior.
   * - Validates age presence.
   * - Maps form answers to the format expected by the API.
   * - Sends answers and age to the scoring endpoint.
   * - Saves the scoring result to display to the user.
   */
  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();

    if (!form.age) {
      alert('Age is required');
      return;
    }

    // Map the question answers from form state to API format
    const answers: Answer[] = questions.map((q) => ({
      id: q.id,
      answer: Boolean(form[`q${q.id}`]), // cast to boolean explicitly
    }));

    try {
      // Call scoring API with answers and numeric age
      const response = await api.post<ScoreResponse>('/score', {
        answers,
        age: parseInt(form.age as string, 10),
      });

      // Set the API response in state to show to user
      setResult(response.data);
    } catch (error) {
      console.error(error);
      alert('Error calculating score.');
    }
  };

  return (
    <div className="form-container">
      <h2>Lifestyle Score Checker</h2>
      <form onSubmit={handleSubmit}>
        {/* Display Age (read-only) */}
        <div className="form-group">
          <label>
            Age:
            <input type="number" name="age" value={form.age as string} readOnly required />
          </label>
        </div>

        {/* Map questions to checkbox inputs */}
        {questions.map((q) => (
          <div className="form-group" key={q.id}>
            <label>
              Q{q.id}. {q.text}
              <input
                type="checkbox"
                name={`q${q.id}`}
                checked={form[`q${q.id}`] as boolean}
                onChange={handleChange}
              />
            </label>
          </div>
        ))}

        {/* Submit button */}
        <button type="submit">Get Score</button>
      </form>

      {/* Show the result if available */}
      {result && (
        <div className="result-box">
          <h3>Score: {result.score}</h3>
          <p>{result.message}</p>
        </div>
      )}
    </div>
  );
};

export default LifestyleForm;
