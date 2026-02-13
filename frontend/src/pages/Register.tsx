import { useRegister } from '../hooks/useRegister';
import { RegisterLayout } from '../components/registers/RegisterLayout';
import { RegisterForm } from '../components/registers/RegisterForm';
import { ErrorMessage } from '../components/commons/ErrorMessage';
import { LoginLink } from '../components/registers/LoginLink';

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