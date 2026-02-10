
import React, { useState } from 'react';

export interface TournamentFormData {
  name: string;
  startDate: string;
  endDate: string;
  bannerUrl: string;
}

interface UseTournamentFormProps {
  onSubmit: (data: TournamentFormData) => Promise<boolean>;
  onCancel: () => void;
}

export const useTournamentForm = ({ onSubmit, onCancel }: UseTournamentFormProps) => {
  const [formData, setFormData] = useState<TournamentFormData>({
    name: '',
    startDate: '',
    endDate: '',
    bannerUrl: 'https://images.unsplash.com/photo-1508098682722-e99c43a406b2?q=80&w=1200&auto=format&fit=crop'
  });
  
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [showSuccess, setShowSuccess] = useState(false);

  const handleChange = (field: keyof TournamentFormData, value: string) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formData.name || !formData.startDate || !formData.endDate) {
      alert("Vänligen fyll i alla obligatoriska fält.");
      return;
    }

    setIsSubmitting(true);
    try {
      const success = await onSubmit(formData);
      if (success) {
        setShowSuccess(true);
        setTimeout(() => {
          setShowSuccess(false);
        }, 2000);
      }
    } catch (err) {
      console.error('Form submission error:', err);
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
