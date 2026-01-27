import seasons from "@/seasons.json"

export default function getLatestSeason() {
    return seasons.reduce((latest, season) => {
        return season.submissionDeadline > latest.submissionDeadline ? season : latest;
    });
}