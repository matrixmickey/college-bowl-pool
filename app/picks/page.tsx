import { auth0 } from "@/lib/auth0";
import { redirect } from "next/navigation";
import getLatestSeason from "../getLatestSeason";
import getAllowPicks from "../getAllowPicks";

export default async function Picks() {
    const session = await auth0.getSession();
    if (!session) {
        redirect("/");
    }
    const latestSeason = getLatestSeason();
    if (!getAllowPicks(latestSeason)) {
        redirect("/");
    }

    return (
        <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
            <div>Your picks page</div>
            <div>
                {latestSeason.games.map((game) => (
                    <div key={game.name} className="mb-4 p-4 border border-gray-300 rounded">
                        <div className="mb-2 font-bold">{game.name}</div>
                        <div className="text-sm text-gray-600">Date: {new Date(game.time).toLocaleString()}</div>
                    </div>
                ))}
            </div>
        </div>
    );
}