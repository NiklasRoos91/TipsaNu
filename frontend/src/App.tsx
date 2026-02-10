
import React from 'react';
import { HashRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { useAuth } from './hooks/useAuth';
import { Layout } from './components/Layout';
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import { TournamentList } from './pages/TournamentList';
import { TournamentDetail } from './pages/TournamentDetail';
import { MatchDetail } from './pages/MatchDetail';
import { ExtraBets } from './pages/ExtraBets';
import { UserProfile } from './pages/UserProfile';
import { Notifications } from './pages/Notifications';
import { CreateTournament } from './pages/CreateTournament';
import { CreateTournamentTemplate } from './pages/CreateTournamentTemplate';
import { TournamentTemplateList } from './pages/TournamentTemplateList';
import { Redirect } from './components/common/Redirect';
import { LoadingScreen } from './components/common/LoadingScreen';

const ProtectedRoute = ({ children }: { children?: React.ReactNode }) => {
  const { isAuthenticated, loading } = useAuth();

  if (loading) return <LoadingScreen />;
  if (!isAuthenticated) return <Redirect to="/login" />;
  console.log('ProtectedRoute render - isAuthenticated:', isAuthenticated, 'loading:', loading);

  return <Layout>{children}</Layout>;
};

const AdminRoute = ({ children }: { children?: React.ReactNode }) => {
  const { isAuthenticated, user, loading } = useAuth();

  if (loading) return <LoadingScreen />;
  if (!isAuthenticated || user?.role !== 'Admin') return <Redirect to="/tournaments" />;

  return <Layout>{children}</Layout>;
};

const App = () => {
  return (
    <AuthProvider>
      <HashRouter>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          
          <Route path="/" element={<Navigate to="/tournaments" replace />} />
          
          <Route path="/tournaments" element={<ProtectedRoute><TournamentList /></ProtectedRoute>} />
          <Route path="/tournaments/new" element={<AdminRoute><CreateTournament /></AdminRoute>} />
          
          {/* Admin specific routes */}
          <Route path="/admin/templates" element={<AdminRoute><TournamentTemplateList /></AdminRoute>} />
          <Route path="/admin/templates/new" element={<AdminRoute><CreateTournamentTemplate /></AdminRoute>} />
          
          <Route path="/tournaments/:id" element={<ProtectedRoute><TournamentDetail /></ProtectedRoute>} />
          <Route path="/matches/:id" element={<ProtectedRoute><MatchDetail /></ProtectedRoute>} />
          
          <Route path="/profile" element={<ProtectedRoute><UserProfile /></ProtectedRoute>} />
          <Route path="/notifications" element={<ProtectedRoute><Notifications /></ProtectedRoute>} />

          <Route path="/leagues" element={<Navigate to="/tournaments" replace />} />
          <Route path="/extrabets" element={<ProtectedRoute><ExtraBets /></ProtectedRoute>} />
          
          <Route path="*" element={<Navigate to="/tournaments" replace />} />
        </Routes>
      </HashRouter>
    </AuthProvider>
  );
};

export default App;
