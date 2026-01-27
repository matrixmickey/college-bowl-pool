export default function getAllowPicks(latestSeason: {submissionDeadline: number}) {
    return latestSeason.submissionDeadline > Date.now()
}