import { useState, useEffect } from 'react';

export const useNotifications = () => {
  const [notifications, setNotifications] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

 // Mock-funktion istället för getNotifications()
  const fetchNotifications = async () => {
    setLoading(true);
    setError(null);
    try {
      // Mock-data
      const data = [
        { id: 1, title: 'Ny match tillagd', message: 'Match mellan Sverige och Tyskland', createdAt: new Date().toISOString() },
        { id: 2, title: 'Turnering startar snart', message: 'VM 2026 börjar om 3 dagar', createdAt: new Date().toISOString() }
      ];
      setNotifications(data);
    } catch (err) {
      setError('Kunde inte hämta notiser');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchNotifications();
  }, []);

  return {
    notifications,
    loading,
    error,
    refresh: fetchNotifications,
  };
};