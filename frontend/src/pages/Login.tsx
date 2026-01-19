import React from 'react';
import { useLogin } from '../hooks/useLogin';
import { LoginLayout } from '../components/login/LoginLayout';
import { LoginForm } from '../components/login/LoginForm';
import { ErrorMessage } from '../components/common/ErrorMessage';
import { QuickLoginButtons } from '../components/login/QuickLoginButtons';
import { RegisterLink } from '../components/login/RegisterLink';

export const Login = () => {
  const {
    email,
    setEmail,
    password,
    setPassword,
    error,
    isLoggingIn,
    handleLogin,
    quickLogin,
  } = useLogin();

  return (
    <LoginLayout>
      <ErrorMessage message={error} />
      
      <LoginForm
        email={email}
        setEmail={setEmail}
        password={password}
        setPassword={setPassword}
        onSubmit={handleLogin}
        isLoggingIn={isLoggingIn}
      />

      <QuickLoginButtons 
        onQuickLogin={quickLogin} 
        isLoggingIn={isLoggingIn} 
      />

      <RegisterLink />
    </LoginLayout>
  );
};