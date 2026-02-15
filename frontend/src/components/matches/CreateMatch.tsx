import React from 'react';

interface CreateMatchProps {
  onCreated?: () => void;
}

export const CreateMatch: React.FC<CreateMatchProps> = ({ onCreated }) => {
  return (
    <div>
      <h3 className="text-lg font-bold text-primary mb-2">Skapa match</h3>
      <p>Här kan du fylla i matchinformation (placeholder)</p>
      <button 
        className="mt-4 px-4 py-2 bg-accent text-white rounded-lg"
        onClick={() => onCreated?.()}
      >
        Stäng
      </button>
    </div>
  );
};

