import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './useAuth';

export const useRegister = () => {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
  });
  const [error, setError] = useState<string | null>(null);
  const [isRegistering, setIsRegistering] = useState(false);
  const { register } = useAuth();
  const navigate = useNavigate();

  const updateField = (field: keyof typeof formData, value: string) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
    if (error) setError(null);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (
      !formData.username.trim() ||
      !formData.email.trim() ||
      !formData.password.trim() ||
      !formData.confirmPassword.trim()
    ) {
      setError('Alla fält måste fyllas i');
      return;
    }

    if (formData.password !== formData.confirmPassword) {
      setError('Lösenorden matchar inte');
      return;
    }

    setIsRegistering(true);
    try {
    const registerData = {
      Email: formData.email,
      Username: formData.username,
      Password: formData.password,
  };
  
    await register(registerData); // använder contextens register
    navigate('/tournaments');
  } catch (err) {
    setError('Registreringen misslyckades. Försök igen.');
  } finally {
    setIsRegistering(false);
  }
};

  return {
    formData,
    updateField,
    error,
    isRegistering,
    handleSubmit,
  };
};