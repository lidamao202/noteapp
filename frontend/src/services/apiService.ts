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
    return axiosInstance.get(`/note/getAll`);
};

export const searchNotes = (title: string) => {
    return axiosInstance.get(`/note/search?title=${title}`);
};

export const deleteNote = (id: string) => {
    return axiosInstance.delete(`/note/${id}`);
};

export const updateNote = (id: string, note: any) => {
    return axiosInstance.put(`/note/${id}`, note);
};

export const getNote = (id: string) => {
    return axiosInstance.get(`/note/getOne/${id}`);
};
export const createNote = (note: any) => {
    return axiosInstance.post('/note', note);
};