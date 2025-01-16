import axiosInstance from '../JwtInterceptor';

export const login = (username: string, password: string) => {
  return axiosInstance.get(`/account/login?username=${username}&password=${password}`);
};

export const register = (userName: string, password: string) => {
  return axiosInstance.post('/account/register', { userName, password }, {
    headers: {
      "Content-Type": "application/json"
    }
  });
};

export const getAllNotes = () => {
  return axiosInstance.get('/notes');
};

export const deleteNote = (id: string) => {
  return axiosInstance.delete(`/notes/${id}`);
};

// Add other API calls here
