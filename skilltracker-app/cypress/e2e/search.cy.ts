describe("search skill profile spec", () => {
  it("passes", () => {
    cy.visit("http://localhost:4200");
    cy.get("a").eq(1).click();
    cy.get("mat-select").click();
    cy.get("mat-option").eq(0).click();
    cy.get("input").type("12");
    cy.get("button").eq(2).click();
    cy.get("h3").contains("Search");
    cy.get("p").contains("No skills found for your search criteria.")
  });
});