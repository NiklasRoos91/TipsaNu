
import React, { useState } from 'react';
import { FormButtons } from '../commons/FormButtons';

interface CreateLeagueFormProps {
  onSubmit: (name: string) => Promise<boolean>;
  onCancel: () => void;
}

export const CreateLeagueForm: React.FC<CreateLeagueFormProps> = ({ onSubmit, onCancel }) => {
  const [newLeagueName, setNewLeagueName] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newLeagueName.trim()) return;

    setIsSubmitting(true);
    const success = await onSubmit(newLeagueName);
    setIsSubmitting(false);

    if (success) {
      setNewLeagueName('');
    } else {
      alert('Kunde inte skapa ligan. Försök igen.');
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="bg-white p-6 rounded-2xl shadow-md border border-slate-200 animate-fade-in"
    >
      <div className="flex flex-col gap-4">
        <div>
          <label className="block text-[10px] font-black text-slate-400 uppercase tracking-widest mb-1.5 ml-1">Ligans Namn</label>
          <input
            type="text"
            value={newLeagueName}
            onChange={(e) => setNewLeagueName(e.target.value)}
            className="w-full px-4 py-3 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all text-slate-900 font-medium placeholder:text-slate-300 shadow-inner"
            placeholder="T.ex. Jobbkompisarna"
            required
            autoFocus
          />
        </div>
        <FormButtons 
          isSubmitting={isSubmitting}
          showSuccess={false}
          onCancel={onCancel}
          isCreate={true}
          saveLabel="Liga"
          layout="row"
        />
      </div>
    </form>
  );
};
