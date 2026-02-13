import { useLogin } from '../hooks/useLogin';
import { LoginLayout } from '../components/logins/LoginLayout';
import { LoginForm } from '../components/logins/LoginForm';
import { ErrorMessage } from '../components/commons/ErrorMessage';
import { QuickLoginButtons } from '../components/logins/QuickLoginButtons';
import { RegisterLink } from '../components/logins/RegisterLink';

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