import type { Route } from "./dtos";

const API_URL = !import.meta.env.DEV ? "http://localhost:5000" : "http://localhost:5136";

/** * Do auth request
 * @param email Email adddress of the user
 * @param password Password of the user
 * @throws @see ErrorResponse Error if the request fails
 */
export const sendAuthRequest = async (username: string, password: string) => {
    return await fetch(`${API_URL}/api/v1/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
    });
}

export const checkAuthStatus = async (token: string | null) => {
    try {
        const response = await fetch(`${API_URL}/api/v1/auth-test`, {
            method: "GET",
            headers: {
                "Authorization": `Basic ${token}`,
            }
        });

        return response.status === 200;
    } catch (ex) {
        return false;
    }
}

export const getRoutes = async () => {
    const response = await fetch(`${API_URL}/api/v1/routes`, {
        method: "GET",
    });

    const data = await response.json() as Route[];
    return data;
}
