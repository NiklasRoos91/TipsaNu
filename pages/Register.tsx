import React from 'react';
import { useRegister } from '../hooks/useRegister';
import { RegisterLayout } from '../components/register/RegisterLayout';
import { RegisterForm } from '../components/register/RegisterForm';
import { ErrorMessage } from '../components/common/ErrorMessage';
import { LoginLink } from '../components/register/LoginLink';

export const Register = () => {
  const {
    formData,
    updateField,
    error,
    isRegistering,
    handleSubmit,
  } = useRegister();

  return (
    <RegisterLayout>
      <ErrorMessage message={error} />
      
      <RegisterForm
        formData={formData}
        onUpdateField={updateField}
        onSubmit={handleSubmit}
        isRegistering={isRegistering}
      />

      <LoginLink />
    </RegisterLayout>
  );
};