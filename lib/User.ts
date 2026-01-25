export default interface User {
    sub: string;
    name?: string;
    nickname?: string;
    given_name?: string;
    family_name?: string;
    picture?: string;
    email?: string;
    email_verified?: boolean;
    /**
     * The organization ID that the user belongs to.
     * This field is populated when the user logs in through an organization.
     */
    org_id?: string;
    [key: string]: any;
}