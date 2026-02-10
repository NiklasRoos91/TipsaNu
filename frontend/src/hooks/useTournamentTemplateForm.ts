
import React, { useState } from 'react';
import { TournamentTemplate, PointRule, TournamentTiebreaker, TiebreakerCriterion } from '../types/types';

export type TournamentTemplateFormData = Omit<TournamentTemplate, 'id'>;

interface UseTournamentTemplateFormProps {
  onSubmit: (data: TournamentTemplateFormData) => Promise<boolean>;
  onCancel: () => void;
}

export const useTournamentTemplateForm = ({ onSubmit, onCancel }: UseTournamentTemplateFormProps) => {
  const [formData, setFormData] = useState<TournamentTemplateFormData>({
    name: '',
    description: '',
    totalGroups: 8,
    advancingPerGroup: 2,
    allowsBestThird: false,
    pointRules: [
      { id: '1', name: 'Standard', pointsForExactScore: 10, pointsForCorrectGoalDifference: 6, pointsForCorrectWinner: 4 }
    ],
    tiebreakers: [
      { criterion: TiebreakerCriterion.GoalDifference, priority: 1 },
      { criterion: TiebreakerCriterion.GoalsScored, priority: 2 }
    ]
  });

  const [isSubmitting, setIsSubmitting] = useState(false);
  const [showSuccess, setShowSuccess] = useState(false);

  const handleChange = (field: keyof TournamentTemplateFormData, value: any) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };

  const validate = (): string | null => {
    if (!formData.name.trim()) return "Namn saknas.";
    if (!formData.description.trim()) return "Beskrivning saknas.";
    if (formData.totalGroups <= 0) return "Antal grupper måste vara minst 1.";
    if (formData.advancingPerGroup <= 0) return "Antal som går vidare måste vara minst 1.";
    return null;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const error = validate();
    if (error) {
      alert(error);
      return;
    }

    setIsSubmitting(true);
    try {
      const success = await onSubmit(formData);
      if (success) {
        setShowSuccess(true);
        setTimeout(() => setShowSuccess(false), 2000);
      }
    } catch (err) {
      console.error('Template submission error:', err);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleCancel = () => {
    onCancel();
  };

  return {
    formData,
    isSubmitting,
    showSuccess,
    handleChange,
    handleSubmit,
    handleCancel
  };
};
