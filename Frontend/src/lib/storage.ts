import type { Runner } from "./dtos";

const defaultRunners = [
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    },
    {
        firstName: "",
        lastName: "",
        speed: 0,
        totalDistance: 0,
    }
]satisfies Runner[];

export const getRunners = (browser: boolean): Runner[] => {
    let runners = defaultRunners;
    if (!browser) return runners;

    const savedJson = localStorage.getItem("runners");
    if (typeof savedJson !== "string") {
        saveRunners(runners);
    } else {
        runners = JSON.parse(savedJson);
        runners.forEach((runner) => {
            runner.totalDistance = 0;
        });
    }
    return runners;
}

export const saveRunners = (runners: Runner[]) => {
    localStorage.setItem("runners", JSON.stringify(runners));
};
