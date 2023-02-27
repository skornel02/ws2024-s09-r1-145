export type ErrorResponse = {
    error: string;
};

export type Route = {
    id: number;
    distance: number;
    startingLocation: string;
    arrivalLocation: string;
    locationName: string;
}

export type ExtendedRoutes = Route & {
    runnerIndex: number;
    time: number;
    timeTotal: number;
}

export type Runner = {
    rev: number;
    firstName: string;
    lastName: string;
    speed: number;
    totalDistance: number;
};
