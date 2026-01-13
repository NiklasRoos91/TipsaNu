import { useState } from 'react';
import { createExtraBet } from '../services/api';
import { ExtraBet } from '../types';

export const useExtraBetForm = (tournamentId: string, onBetCreated: (eb: ExtraBet) => void) => {
  const [showForm, setShowForm] = useState(false);
  const [isCreating, setIsCreating] = useState(false);
  const [formData, setFormData] = useState({
    question: '',
    description: '',
    points: 10,
    deadline: '',
    optionsString: '',
    allowCustom: false
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formData.question || !formData.deadline) {
      alert("Fyll i alla obligatoriska fält.");
      return;
    }

    const options = formData.optionsString.split(',').map(s => s.trim()).filter(s => s.length > 0);
    if (options.length === 0 && !formData.allowCustom) {
      alert("Ange antingen alternativ eller tillåt fritext.");
      return;
    }

    setIsCreating(true);
    try {
      const newBet = await createExtraBet({
        tournamentId,
        question: formData.question,
        description: formData.description,
        points: Number(formData.points),
        deadline: new Date(formData.deadline).toISOString(),
        allowedValues: options.length > 0 ? options : undefined,
        requiresValue: formData.allowCustom
      });

      onBetCreated(newBet);
      setShowForm(false);
      setFormData({ question: '', description: '', points: 10, deadline: '', optionsString: '', allowCustom: false });
    } catch (e) {
      alert("Kunde inte skapa extra bet.");
    } finally {
      setIsCreating(false);
    }
  };

  return {
    showForm, setShowForm,
    isCreating,
    formData, setFormData,
    handleSubmit
  };
};