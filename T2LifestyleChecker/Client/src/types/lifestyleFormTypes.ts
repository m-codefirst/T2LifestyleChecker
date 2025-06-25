// Type for a single question
export interface Question {
  id: number;
  text: string;
}

// Type for an individual answer
export interface Answer {
  id: number;
  answer: boolean;
}

// Props accepted by LifestyleForm component
export interface LifestyleFormProps {
  age: number;
}

// API response shape after scoring
export interface ScoreResponse {
  score: number;
  message: string;
}
