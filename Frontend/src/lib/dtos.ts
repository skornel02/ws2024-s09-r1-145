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
    firstName: string;
    lastName: string;
    speed: number;
    totalDistance: number;
};
