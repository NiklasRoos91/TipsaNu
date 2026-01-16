import React from 'react';

interface ErrorMessageProps {
  message?: string | null;
}

export const ErrorMessage: React.FC<ErrorMessageProps> = ({ message }) => {
  if (!message) return null;
  return (
    <div className="bg-red-50 text-red-600 p-3 rounded-lg mb-4 text-sm text-center font-medium animate-fade-in border border-red-100">
      {message}
    </div>
  );
};